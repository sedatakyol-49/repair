export interface Customer {
  id?: string;
  name: string;
  email: string;
  phone: string;
}

export interface Product {
  id?: string;
  type: string;
  brand: string;
  model: string;
  serialNumber?: string;
}

export interface RepairStatus {
  id: string;
  status: 'received' | 'diagnosing' | 'repairing' | 'completed' | 'delivered';
  timestamp: Date;
  notes?: string;
}

export interface Repair {
  id?: string;
  customer: Customer;
  product: Product;
  description: string;
  receivedImages: string[];
  completedImages?: string[];
  status: RepairStatus[];
  createdAt: Date;
  updatedAt: Date;
  estimatedCompletionDate: Date;
  appointmentDate: Date;
  appointmentTime: string;
}