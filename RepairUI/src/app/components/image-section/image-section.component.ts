import { Component, Input } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ImageSection } from "../../utils/image.utils";

@Component({
  selector: "app-image-section",
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="bg-gray-50 p-4 rounded-lg">
      <h3 class="text-lg font-semibold text-gray-800 mb-4">
        {{ section.title }}
      </h3>
      <div class="space-y-4">
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-2"
            >Upload Images</label
          >
          <input
            type="file"
            multiple
            accept="image/*"
            (change)="section.onSelect($event)"
            class="w-full"
          />
          <p class="text-sm text-gray-500 mt-1">{{ section.description }}</p>
        </div>

        <!-- Image Previews -->
        <div
          *ngIf="section.images.length > 0"
          class="grid grid-cols-2 md:grid-cols-3 gap-4"
        >
          <div
            *ngFor="let url of section.images; let i = index"
            class="relative"
          >
            <img [src]="url" class="w-full h-32 object-cover rounded-lg" />
            <button
              type="button"
              (click)="section.onRemove(i)"
              class="absolute top-2 right-2 bg-red-500 text-white rounded-full p-1 hover:bg-red-600"
            >
              <svg
                class="w-4 h-4"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M6 18L18 6M6 6l12 12"
                />
              </svg>
            </button>
          </div>
        </div>
      </div>
    </div>
  `,
})
export class ImageSectionComponent {
  @Input() section!: ImageSection;
}
