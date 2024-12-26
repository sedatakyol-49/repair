import { Injectable } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor(private api: ApiService) {}

  // Note: These methods are now handled by the backend
  // They remain here for compatibility but don't need to do anything
  // as notifications are sent automatically when status is updated
  
  async sendEmail(to: string, subject: string, body: string): Promise<void> {
    console.log('Email notifications are handled by the backend');
  }

  async sendSMS(to: string, message: string): Promise<void> {
    console.log('SMS notifications are handled by the backend');
  }

  async notifyCustomer(repair: any, newStatus: string): Promise<void> {
    console.log('Customer notifications are handled by the backend');
  }
}