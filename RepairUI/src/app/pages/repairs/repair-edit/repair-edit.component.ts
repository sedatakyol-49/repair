import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { RepairService } from "../../../services/repair.service";
import { map } from "rxjs/operators";
import {
  handleImageUpload,
  removeImage,
  getImageUrl,
  ImageSection,
} from "../../../utils/image.utils";
import { createRepairForm } from "../repair-create/repair-create.form";
import {
  getCurrentStatus,
  getStatusText,
  getStatusClass,
} from "../../../utils/repair-status.utils";
import { firstValueFrom } from "rxjs";
import { RepairStatusType } from "../../../models/repair.model";
import { ImageSectionComponent } from "../../../components/image-section/image-section.component";

@Component({
  selector: "app-repair-edit",
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    ImageSectionComponent,
  ],
  templateUrl: "./repair-edit.component.html",
  styleUrls: ["./repair-edit.component.css"],
})
export class RepairEditComponent implements OnInit {
  repairForm: FormGroup = createRepairForm(this.fb);
  receivedImageUrls: string[] = [];
  receivedImages: File[] = [];
  completedImageUrls: string[] = [];
  completedImages: File[] = [];

  receivedSection: ImageSection = {
    title: "Initial Device Images",
    description: "Images of the device when received",
    images: this.receivedImageUrls,
    onSelect: (event) => this.onReceivedImagesSelect(event),
    onRemove: (index) => this.removeReceivedImage(index),
  };

  completedSection: ImageSection = {
    title: "Completed Repair Images",
    description: "Images of the device after repair completion",
    images: this.completedImageUrls,
    onSelect: (event) => this.onCompletedImagesSelect(event),
    onRemove: (index) => this.removeCompletedImage(index),
  };

  statusOptions = [
    { value: "received", label: "Received" },
    { value: "diagnosing", label: "Diagnosing" },
    { value: "repairing", label: "Repairing" },
    { value: "completed", label: "Completed" },
    { value: "delivered", label: "Delivered" },
  ];

  repair$ = this.repairService.repairs$.pipe(
    map((repairs) => {
      const id = this.route.snapshot.paramMap.get("id");
      return repairs.find((repair) => repair.id === id);
    })
  );

  constructor(
    private fb: FormBuilder,
    private repairService: RepairService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.repair$.subscribe((result) => {
      this.receivedImageUrls = result?.receivedImages || [];
      this.completedImageUrls = result?.completedImages || [];
      this.receivedSection.images = this.receivedImageUrls;
      this.completedSection.images = this.completedImageUrls;
    });
  }

  ngOnInit() {
    this.repair$.subscribe((repair) => {
      if (repair) {
        this.repairForm.patchValue({
          description: repair.description,
          name: repair.name,
          surname: repair.surname,
          email: repair.email,
          phone: repair.phone,
          type: repair.type,
          brand: repair.brand,
          model: repair.model,
          serialNumber: repair.serialNumber,
          appointmentDate: this.formatDate(repair.appointmentDate),
          appointmentTime: repair.appointmentTime,
          estimatedCompletionDate: this.formatDate(
            repair.estimatedCompletionDate
          ),
        });
      }
    });
  }

  // Utility functions
  getCurrentStatus = getCurrentStatus;
  getStatusText = getStatusText;
  getStatusClass = getStatusClass;
  getImageUrl = getImageUrl;

  private formatDate(date: Date): string {
    return new Date(date).toISOString().split("T")[0];
  }

  onReceivedImagesSelect(event: Event): void {
    const result = handleImageUpload(event);
    if (result) {
      this.receivedImages = result.files;
      this.receivedImageUrls = result.previewUrls;
      this.receivedSection.images = this.receivedImageUrls;
    }
  }

  onCompletedImagesSelect(event: Event): void {
    const result = handleImageUpload(event);
    if (result) {
      this.completedImages = result.files;
      this.completedImageUrls = result.previewUrls;
      this.completedSection.images = this.completedImageUrls;
    }
  }

  removeReceivedImage(index: number): void {
    const result = removeImage(
      index,
      this.receivedImages,
      this.receivedImageUrls
    );
    this.receivedImages = result.files;
    this.receivedImageUrls = result.previewUrls;
    this.receivedSection.images = this.receivedImageUrls;
  }

  removeCompletedImage(index: number): void {
    const result = removeImage(
      index,
      this.completedImages,
      this.completedImageUrls
    );
    this.completedImages = result.files;
    this.completedImageUrls = result.previewUrls;
    this.completedSection.images = this.completedImageUrls;
  }

  removeImage(index: number): void {
    const result = removeImage(
      index,
      this.receivedImages,
      this.receivedImageUrls
    );
    this.receivedImages = result.files;
    this.receivedImageUrls = result.previewUrls;
  }

  updateStatus(newStatus: RepairStatusType, repairId: string | undefined) {
    if (!repairId) return;
    this.repairService
      .updateRepairStatus(repairId, newStatus, "Status updated from edit view")
      .subscribe();
  }

  cancel() {
    const id = this.route.snapshot.paramMap.get("id");
    this.router.navigate(["/repairs", id]);
  }

  async onSubmit() {
    if (this.repairForm.valid) {
      const id = this.route.snapshot.paramMap.get("id");
      if (id) {
        const formData = this.repairForm.value;
        const repair = {
          ...formData,
          id: this.repairService.getRepair(id)?.id,
          receivedImages: this.receivedImageUrls,
          completedImages: this.completedImageUrls,
        };

        try {
          await firstValueFrom(this.repairService.updateRepair(id, repair));
          this.router.navigate(["/repairs", id]);
        } catch (error: unknown) {
          console.error("Error updating repair:", error);
        }
      }
    }
  }
}
