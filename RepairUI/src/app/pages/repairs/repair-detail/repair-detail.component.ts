import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { RepairService } from '../../../services/repair.service';
import { map } from 'rxjs/operators';
import { getCurrentStatus, getStatusText, getStatusClass } from '../../../utils/repair-status.utils';
import { getImageUrl } from '../../../utils/image.utils';

@Component({
  selector: 'app-repair-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './repair-detail.component.html',
  styleUrls: ['./repair-detail.component.css']
})
export class RepairDetailComponent {
  repair$ = this.repairService.repairs$.pipe(
    map(repairs => {
      const id = this.route.snapshot.paramMap.get('id');
      return repairs.find(repair => repair.id === id);
    })
  );

  statusOptions = [
    { value: 'received', label: 'Received' },
    { value: 'diagnosing', label: 'Diagnosing' },
    { value: 'repairing', label: 'Repairing' },
    { value: 'completed', label: 'Completed' },
    { value: 'delivered', label: 'Delivered' }
  ];

  constructor(
    private repairService: RepairService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  getCurrentStatus = getCurrentStatus;
  getStatusText = getStatusText;
  getStatusClass = getStatusClass;
  getImageUrl = getImageUrl;

  updateStatus(event: Event, repairId: string | undefined) {
    if (!repairId) return;
    const select = event.target as HTMLSelectElement;
    this.repairService.updateRepairStatus(repairId, select.value, 'Status updated');
  }
}