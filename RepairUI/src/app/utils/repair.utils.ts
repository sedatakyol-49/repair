export const getStatusText = (status: string): string => {
  const statusMap: { [key: string]: string } = {
    'received': 'Received',
    'diagnosing': 'Diagnosing',
    'repairing': 'Repairing',
    'completed': 'Completed',
    'delivered': 'Delivered'
  };
  return statusMap[status] || status;
};

export const getProductTypeText = (type: string): string => {
  const typeMap: { [key: string]: string } = {
    'phone': 'Phone',
    'tablet': 'Tablet',
    'laptop': 'Laptop'
  };
  return typeMap[type] || type;
};