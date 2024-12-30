export interface RepairStatus {
  id: string;
  status: RepairStatusType;
  timestamp: string;
  notes?: string;
  repairId: string;
}

export interface Repair {
  id?: string;
  description: string;
  receivedImages?: string[];
  completedImages?: string[];
  createdAt: Date;
  updatedAt: Date;
  estimatedCompletionDate: Date;
  appointmentDate: Date;
  appointmentTime: string;
  name: string;
  surname: string;
  email: string;
  phone: string;
  type: string;
  brand: string;
  model?: string;
  serialNumber?: string;
  statusHistory: RepairStatus[];
}

export type RepairStatusType =
  | "received"
  | "diagnosing"
  | "repairing"
  | "completed"
  | "delivered";

export interface UpdateStatusRequest {
  status: RepairStatusType;
  notes?: string;
}
