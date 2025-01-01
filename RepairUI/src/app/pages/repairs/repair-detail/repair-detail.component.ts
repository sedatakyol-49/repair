import { Component, OnDestroy } from "@angular/core";
import { AsyncPipe, CommonModule } from "@angular/common";
import { ActivatedRoute, RouterLink } from "@angular/router";
import { RepairService } from "../../../services/repair.service";
import { map, takeUntil } from "rxjs/operators";
import {
  getCurrentStatus,
  getStatusText,
  getStatusClass,
} from "../../../utils/repair-status.utils";
import { getImageUrl } from "../../../utils/image.utils";
import { Subject } from "rxjs";

@Component({
  selector: "app-repair-detail",
  standalone: true,
  imports: [CommonModule, RouterLink, AsyncPipe],
  templateUrl: "./repair-detail.component.html",
  styleUrls: ["./repair-detail.component.css"],
})
export class RepairDetailComponent implements OnDestroy {
  private destroy$ = new Subject<void>();
  repair$ = this.repairService.repairs$.pipe(
    takeUntil(this.destroy$),
    map((repairs) => {
      const id = this.route.snapshot.paramMap.get("id");
      return repairs.find((repair) => repair.id === id);
    })
  );

  receivedImages$ = this.repairService.receivedImages$;
  completedImages$ = this.repairService.completedImages$;

  statusOptions = [
    { value: "received", label: "Received" },
    { value: "diagnosing", label: "Diagnosing" },
    { value: "repairing", label: "Repairing" },
    { value: "completed", label: "Completed" },
    { value: "delivered", label: "Delivered" },
  ];

  constructor(
    private repairService: RepairService,
    private route: ActivatedRoute
  ) {
    const id = this.route.snapshot.paramMap.get("id");
    if (id) {
      this.repairService.loadReceivedImages(id);
      this.repairService.loadCompletedImagess(id);
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  getCurrentStatus = getCurrentStatus;
  getStatusText = getStatusText;
  getStatusClass = getStatusClass;
  getImageUrl = getImageUrl;

  updateStatus(event: Event, repairId: string | undefined) {
    if (!repairId) return;
    const select = event.target as HTMLSelectElement;
    this.repairService
      .updateRepairStatus(repairId, select.value, "Status updated")
      .pipe(takeUntil(this.destroy$));
  }
}
