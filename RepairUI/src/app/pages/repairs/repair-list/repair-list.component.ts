import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { RouterLink } from "@angular/router";
import { RepairService } from "../../../services/repair.service";
import { RepairFilterPipe } from "../../../pipes/repair-filter.pipe";
import {
  getStatusText,
  getStatusClass,
  getCurrentStatus,
} from "../../../utils/repair-status.utils";
import { TranslatePipe } from "../../../pipes/translate.pipe";

@Component({
  selector: "app-repair-list",
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterLink,
    RepairFilterPipe,
    TranslatePipe,
  ],
  templateUrl: "./repair-list.component.html",
  styleUrls: ["./repair-list.component.css"],
})
export class RepairListComponent {
  repairs$ = this.repairService.repairs$;

  filters = {
    customerName: "",
    phone: "",
    productType: "",
    status: "",
  };

  statusOptions = [
    { value: "received", label: "Received" },
    { value: "diagnosing", label: "Diagnosing" },
    { value: "repairing", label: "Repairing" },
    { value: "completed", label: "Completed" },
    { value: "delivered", label: "Delivered" },
  ];

  constructor(private repairService: RepairService) {}

  getStatusText = getStatusText;
  getStatusClass = getStatusClass;
  getCurrentStatus = getCurrentStatus;

  updateStatus(event: Event, repairId: string | undefined) {
    if (!repairId) return;

    event.stopPropagation();
    const select = event.target as HTMLSelectElement;
    this.repairService
      .updateRepairStatus(
        repairId,
        select.value,
        "Status updated from list view"
      )
      .subscribe();
  }
}
