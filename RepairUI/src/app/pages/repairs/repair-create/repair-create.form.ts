import { FormBuilder, FormGroup, Validators } from '@angular/forms';

export function createRepairForm(fb: FormBuilder): FormGroup {
  return fb.group({
    customer: fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required]
    }),
    product: fb.group({
      type: ['', Validators.required],
      brand: ['', Validators.required],
      model: ['', Validators.required],
      serialNumber: ['']
    }),
    description: ['', Validators.required],
    appointmentDate: ['', Validators.required],
    appointmentTime: ['', Validators.required],
    estimatedCompletionDate: ['', Validators.required]
  });
}