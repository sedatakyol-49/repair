import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormBuilder, FormGroup, ReactiveFormsModule } from "@angular/forms";
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

@Component({
  selector: "app-repair-edit",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
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
          product: repair.product,
          description: repair.description,
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

  updateStatus(event: Event, repairId: string | undefined) {
    if (!repairId) return;
    const select = event.target as HTMLSelectElement;
    this.repairService.updateRepairStatus(
      repairId,
      select.value,
      "Status updated from edit view"
    );
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
          receivedImages:
            this.imagePreviewUrls.length > 0
              ? this.imagePreviewUrls
              : this.repairService.getRepair(id)?.receivedImages || [],
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
