import { Pipe, PipeTransform } from '@angular/core';
import { Repair } from '../../../models/repair.model';

interface Filters {
  customerName: string;
  phone: string;
  productType: string;
  status: string;
}

@Pipe({
  name: 'repairFilter',
  standalone: true,
  pure: false // Make the pipe impure to handle object changes
})
export class RepairFilterPipe implements PipeTransform {
  transform(repairs: Repair[] | null, filters: Filters): Repair[] {
    if (!repairs) return [];
    
    return repairs.filter(repair => {
      const currentStatus = repair.status[repair.status.length - 1].status;
      
      const matchesName = !filters.customerName || 
        repair.customer.name.toLowerCase().includes(filters.customerName.toLowerCase());
      
      const matchesPhone = !filters.phone || 
        repair.customer.phone.includes(filters.phone);
      
      const matchesType = !filters.productType || 
        repair.product.type === filters.productType;
      
      const matchesStatus = !filters.status || 
        currentStatus === filters.status;

      return matchesName && matchesPhone && matchesType && matchesStatus;
    });
  }
}