import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-about',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="min-h-screen bg-gray-50 py-12">
      <div class="container mx-auto px-4 max-w-4xl">
        <h1 class="text-4xl font-bold mb-8">Hakkımızda</h1>
        
        <div class="bg-white rounded-lg shadow-md p-8">
          <p class="text-lg mb-6">
            Arıza Takip Sistemi, işletmelerin müşterilerine daha iyi hizmet verebilmesi için tasarlanmış profesyonel bir yönetim platformudur.
          </p>
          
          <h2 class="text-2xl font-semibold mb-4">Neler Sunuyoruz?</h2>
          <ul class="list-disc list-inside space-y-2 mb-6">
            <li>Kolay arıza kaydı ve takibi</li>
            <li>Müşteri bilgilendirme sistemi</li>
            <li>Fotoğraflı belgelendirme</li>
            <li>Detaylı raporlama</li>
            <li>7/24 teknik destek</li>
          </ul>
          
          <h2 class="text-2xl font-semibold mb-4">Misyonumuz</h2>
          <p class="text-lg mb-6">
            İşletmelerin teknik servis süreçlerini dijitalleştirerek müşteri memnuniyetini artırmak ve iş süreçlerini optimize etmek.
          </p>
        </div>
      </div>
    </div>
  `
})
export class AboutComponent {}