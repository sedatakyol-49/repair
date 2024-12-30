import { Repair } from "../models/repair.model";

export const getCurrentStatus = (repair: Repair): string => {
  if (!repair?.statusHistory?.length) {
    return "received";
  }
  // Find the status with the latest timestamp
  const latestStatus = repair.statusHistory.reduce((acc, curr) => {
    return curr.timestamp > acc.timestamp ? curr : acc;
  });
  return latestStatus.status;
};

export const getStatusText = (status: string): string => {
  const statusMap: { [key: string]: string } = {
    received: "Received",
    diagnosing: "Diagnosing",
    repairing: "Repairing",
    completed: "Completed",
    delivered: "Delivered",
  };
  return statusMap[status] || status;
};

export const getStatusClass = (status: string): { [key: string]: boolean } => {
  const baseClasses = {
    "px-3": true,
    "py-1": true,
    "rounded-full": true,
    "text-sm": true,
    "font-medium": true,
  };

  const statusClasses: { [key: string]: { [key: string]: boolean } } = {
    received: {
      ...baseClasses,
      "bg-yellow-100": true,
      "text-yellow-800": true,
    },
    diagnosing: { ...baseClasses, "bg-blue-100": true, "text-blue-800": true },
    repairing: {
      ...baseClasses,
      "bg-purple-100": true,
      "text-purple-800": true,
    },
    completed: { ...baseClasses, "bg-green-100": true, "text-green-800": true },
    delivered: { ...baseClasses, "bg-gray-100": true, "text-gray-800": true },
  };

  return statusClasses[status] || baseClasses;
};
