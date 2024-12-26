import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="min-h-screen bg-gray-50">
      <!-- Hero Section -->
      <div class="bg-blue-600 text-white">
        <div class="container mx-auto px-4 py-16">
          <div class="max-w-3xl mx-auto text-center">
            <h1 class="text-4xl md:text-5xl font-bold mb-6">
              Arıza Takip Sistemi
            </h1>
            <p class="text-xl mb-8">
              İşletmenizin teknik servis süreçlerini dijitalleştirin, müşteri memnuniyetini artırın.
            </p>
            <div class="space-x-4">
              <a routerLink="/register" 
                 class="bg-white text-blue-600 px-6 py-3 rounded-lg font-medium hover:bg-blue-50">
                Hemen Başlayın
              </a>
              <a routerLink="/about" 
                 class="border border-white px-6 py-3 rounded-lg font-medium hover:bg-blue-700">
                Detaylı Bilgi
              </a>
            </div>
          </div>
        </div>
      </div>

      <!-- Features -->
      <div class="container mx-auto px-4 py-16">
        <h2 class="text-3xl font-bold text-center mb-12">Özellikler</h2>
        
        <div class="grid md:grid-cols-3 gap-8">
          <div class="bg-white p-6 rounded-lg shadow-md">
            <div class="w-12 h-12 bg-blue-100 text-blue-600 rounded-lg flex items-center justify-center mb-4">
              <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2" />
              </svg>
            </div>
            <h3 class="text-xl font-semibold mb-2">Kolay Takip</h3>
            <p class="text-gray-600">
              Arıza kayıtlarını kolayca oluşturun ve takip edin. Müşterilerinizi otomatik olarak bilgilendirin.
            </p>
          </div>

          <div class="bg-white p-6 rounded-lg shadow-md">
            <div class="w-12 h-12 bg-blue-100 text-blue-600 rounded-lg flex items-center justify-center mb-4">
              <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 9a2 2 0 012-2h.93a2 2 0 001.664-.89l.812-1.22A2 2 0 0110.07 4h3.86a2 2 0 011.664.89l.812 1.22A2 2 0 0018.07 7H19a2 2 0 012 2v9a2 2 0 01-2 2H5a2 2 0 01-2-2V9z" />
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 13a3 3 0 11-6 0 3 3 0 016 0z" />
              </svg>
            </div>
            <h3 class="text-xl font-semibold mb-2">Fotoğraflı Kayıt</h3>
            <p class="text-gray-600">
              Cihazların teslim alınma ve tamir sonrası durumlarını fotoğraflarla belgelendirin.
            </p>
          </div>

          <div class="bg-white p-6 rounded-lg shadow-md">
            <div class="w-12 h-12 bg-blue-100 text-blue-600 rounded-lg flex items-center justify-center mb-4">
              <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9" />
              </svg>
            </div>
            <h3 class="text-xl font-semibold mb-2">Otomatik Bilgilendirme</h3>
            <p class="text-gray-600">
              Müşterilerinizi SMS ve e-posta ile otomatik olarak bilgilendirin, memnuniyeti artırın.
            </p>
          </div>
        </div>
      </div>
    </div>
  `
})
export class HomeComponent {}