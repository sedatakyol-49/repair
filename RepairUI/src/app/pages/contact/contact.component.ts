import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="min-h-screen bg-gray-50 py-12">
      <div class="container mx-auto px-4 max-w-4xl">
        <h1 class="text-4xl font-bold mb-8">İletişim</h1>
        
        <div class="grid md:grid-cols-2 gap-8">
          <div class="bg-white rounded-lg shadow-md p-8">
            <h2 class="text-2xl font-semibold mb-4">Bize Ulaşın</h2>
            
            <form [formGroup]="contactForm" (ngSubmit)="onSubmit()" class="space-y-4">
              <div>
                <label class="block text-sm font-medium text-gray-700">Ad Soyad</label>
                <input type="text" 
                       formControlName="name"
                       class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500">
              </div>
              
              <div>
                <label class="block text-sm font-medium text-gray-700">E-posta</label>
                <input type="email" 
                       formControlName="email"
                       class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500">
              </div>
              
              <div>
                <label class="block text-sm font-medium text-gray-700">Mesajınız</label>
                <textarea formControlName="message"
                          rows="4"
                          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"></textarea>
              </div>
              
              <button type="submit"
                      [disabled]="contactForm.invalid"
                      class="w-full bg-blue-600 text-white py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2">
                Gönder
              </button>
            </form>
          </div>
          
          <div class="bg-white rounded-lg shadow-md p-8">
            <h2 class="text-2xl font-semibold mb-4">İletişim Bilgileri</h2>
            
            <div class="space-y-4">
              <div>
                <h3 class="font-medium text-gray-700">Adres</h3>
                <p>Örnek Mahallesi, Teknoloji Caddesi No:123<br/>34000 İstanbul</p>
              </div>
              
              <div>
                <h3 class="font-medium text-gray-700">Telefon</h3>
                <p>+90 (212) 123 45 67</p>
              </div>
              
              <div>
                <h3 class="font-medium text-gray-700">E-posta</h3>
                <p>infoarizatakip.com</p>
              </div>
              
              <div>
                <h3 class="font-medium text-gray-700">Çalışma Saatleri</h3>
                <p>Pazartesi - Cumartesi: 09:00 - 18:00</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class ContactComponent {
  contactForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.contactForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      message: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.contactForm.valid) {
      console.log('Form submitted:', this.contactForm.value);
      // İletişim form gönderimi burada implemente edilecek
      this.contactForm.reset();
    }
  }
}