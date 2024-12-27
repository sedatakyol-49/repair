import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormBuilder, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { Router } from "@angular/router";
import { RepairService } from "../../../services/repair.service";
import { createRepairForm } from "./repair-create.form";
import { handleImageUpload, removeImage } from "../../../utils/image.utils";
import { firstValueFrom } from "rxjs";

@Component({
  selector: "app-repair-create",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: "./repair-create.component.html",
})
export class RepairCreateComponent {
  repairForm: FormGroup = createRepairForm(this.fb);
  imagePreviewUrls: string[] = [];
  receivedImages: File[] = [];

  constructor(
    private fb: FormBuilder,
    private repairService: RepairService,
    private router: Router
  ) {}

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

  cancel(): void {
    this.router.navigate(["/repairs"]);
  }

  async onSubmit(): Promise<void> {
    if (this.repairForm.valid) {
      const formData = this.repairForm.value;

      const repair = {
        ...formData,
        receivedImages: this.imagePreviewUrls,
        createdAt: new Date(),
        updatedAt: new Date(),
      };

      try {
        await firstValueFrom(this.repairService.createRepair(repair));
        this.router.navigate(["/repairs"]);
      } catch (error: unknown) {
        console.error("Error creating repair:", error);
      }
    }
  }
}
