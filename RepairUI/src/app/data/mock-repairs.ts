import { Repair } from "../models/repair.model";

export const mockRepairs: Repair[] = [
  {
    id: "1",
    customer: {
      name: "John Smith",
      email: "john@example.com",
      phone: "555-0123",
    },
    product: {
      type: "phone",
      brand: "iPhone",
      model: "13 Pro",
      serialNumber: "ABC123",
    },
    description: "Screen is cracked and battery drains quickly",
    receivedImages: ["https://example.com/image1.jpg"],
    statusHistory: [
      {
        id: "1",
        status: "received",
        timestamp: new Date("2024-02-20"),
        notes: "Device received with visible screen damage",
      },
    ],
    createdAt: new Date("2024-02-20"),
    updatedAt: new Date("2024-02-20"),
    estimatedCompletionDate: new Date("2024-02-23"),
    appointmentDate: new Date("2024-02-20"),
    appointmentTime: "10:00",
  },
  {
    id: "2",
    customer: {
      name: "Sarah Johnson",
      email: "sarah@example.com",
      phone: "555-0456",
    },
    product: {
      type: "laptop",
      brand: "Dell",
      model: "XPS 15",
      serialNumber: "XYZ789",
    },
    description: "Won't turn on, possible motherboard issue",
    receivedImages: ["https://example.com/image2.jpg"],
    statusHistory: [
      {
        id: "2",
        status: "diagnosing",
        timestamp: new Date("2024-02-19"),
        notes: "Initial diagnosis shows potential power supply issue",
      },
    ],
    createdAt: new Date("2024-02-19"),
    updatedAt: new Date("2024-02-19"),
    estimatedCompletionDate: new Date("2024-02-22"),
    appointmentDate: new Date("2024-02-19"),
    appointmentTime: "14:30",
  },
];
