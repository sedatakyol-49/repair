import { FormBuilder, FormGroup, Validators } from "@angular/forms";

export function createRepairForm(fb: FormBuilder): FormGroup {
  return fb.group({
    name: ["", Validators.required],
    surname: ["", Validators.required],
    email: ["", [Validators.required, Validators.email]],
    phone: ["", Validators.required],
    type: ["", Validators.required],
    brand: ["", Validators.required],
    model: ["", Validators.required],
    serialNumber: [""],
    description: ["", Validators.required],
    appointmentDate: ["", Validators.required],
    appointmentTime: ["", Validators.required],
    estimatedCompletionDate: ["", Validators.required],
  });
}
