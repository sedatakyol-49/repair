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
} from "../../../utils/image.utils";
import { createRepairForm } from "../repair-create/repair-create.form";
import {
  getCurrentStatus,
  getStatusText,
  getStatusClass,
} from "../../../utils/repair-status.utils";
import { firstValueFrom } from "rxjs";
import { RepairStatusType } from "../../../models/repair.model";

@Component({
  selector: "app-repair-edit",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: "./repair-edit.component.html",
  styleUrls: ["./repair-edit.component.css"],
})
export class RepairEditComponent implements OnInit {
  repairForm: FormGroup = createRepairForm(this.fb);
  imagePreviewUrls: string[] = [];
  receivedImages: File[] = [];

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
  ) {}

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
      this.imagePreviewUrls = result.previewUrls;
    }
  }

  removeImage(index: number): void {
    const result = removeImage(
      index,
      this.receivedImages,
      this.imagePreviewUrls
    );
    this.receivedImages = result.files;
    this.imagePreviewUrls = result.previewUrls;
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
          createdAt: this.repairService.getRepair(id)?.createdAt,
          updatedAt: new Date(),
          receivedImages:
            this.imagePreviewUrls.length > 0
              ? this.imagePreviewUrls
              : this.repairService.getRepair(id)?.receivedImages || [],
          completedImages: this.receivedImages,
          statusHistory: this.repairService.getRepair(id)?.statusHistory || [],
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
