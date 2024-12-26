import { SafeUrl, DomSanitizer } from '@angular/platform-browser';

export interface ImageUploadResult {
  files: File[];
  previewUrls: string[];
}

export function handleImageUpload(event: Event): ImageUploadResult | null {
  const input = event.target as HTMLInputElement;
  if (!input.files?.length) return null;

  const files = Array.from(input.files);
  const previewUrls: string[] = [];

  files.forEach(file => {
    const reader = new FileReader();
    reader.onload = (e: ProgressEvent<FileReader>) => {
      if (e.target?.result) {
        previewUrls.push(e.target.result as string);
      }
    };
    reader.readAsDataURL(file);
  });

  return { files, previewUrls };
}

export function removeImage(
  index: number,
  currentFiles: File[],
  currentUrls: string[]
): ImageUploadResult {
  const files = [...currentFiles];
  const previewUrls = [...currentUrls];
  
  files.splice(index, 1);
  previewUrls.splice(index, 1);
  
  return { files, previewUrls };
}

export function isDataUrl(url: string): boolean {
  return url.startsWith('data:');
}

export function getImageUrl(url: string): string {
  return isDataUrl(url) ? url : url;
}