import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, tap } from "rxjs";
import { Repair } from "../models/repair.model";
import { ApiService } from "./api.service";

@Injectable({
  providedIn: "root",
})
export class RepairService {
  private repairs = new BehaviorSubject<Repair[]>([]);
  repairs$ = this.repairs.asObservable();

  private receivedImages = new BehaviorSubject<string[]>([]);
  receivedImages$ = this.receivedImages.asObservable();

  private completedImages = new BehaviorSubject<string[]>([]);
  completedImages$ = this.completedImages.asObservable();

  constructor(private api: ApiService) {
    this.loadRepairs();
  }

  private loadRepairs() {
    this.api
      .get<Repair[]>("repairs")
      .pipe()
      .subscribe((repairs) => this.repairs.next(repairs));
  }

  public loadReceivedImages(id: string) {
    this.api
      .get<string[]>(`repairs/${id}/received-images`)
      .pipe()
      .subscribe((receivedImages) => this.receivedImages.next(receivedImages));
  }

  public loadCompletedImagess(id: string) {
    this.api
      .get<string[]>(`repairs/${id}/completed-images`)
      .pipe()
      .subscribe((completedImages) =>
        this.completedImages.next(completedImages)
      );
  }

  createRepair(repair: Partial<Repair>): Observable<Repair> {
    return this.api.post<Repair>("repairs", repair).pipe(
      tap((newRepair) => {
        const currentRepairs = this.repairs.getValue();
        this.repairs.next([...currentRepairs, newRepair]);
      })
    );
  }

  updateRepair(id: string, repair: Partial<Repair>): Observable<void> {
    console.log("repair:", repair);
    return this.api.put<void>(`repairs/${id}`, repair).pipe(
      tap(() => {
        const currentRepairs = this.repairs.getValue();
        const updatedRepairs = currentRepairs.map((r) =>
          r.id === id ? { ...r, ...repair } : r
        );
        this.repairs.next(updatedRepairs);
      })
    );
  }

  updateRepairStatus(
    id: string,
    status: string,
    notes?: string
  ): Observable<void> {
    return this.api.put<void>(`repairs/${id}/status`, { status, notes }).pipe(
      tap(() => {
        const currentRepairs = this.repairs.getValue();
        const updatedRepairs = currentRepairs.map((repair) => {
          if (repair.id === id) {
            return {
              ...repair,
              statusHistory: [
                ...repair.statusHistory,
                {
                  id: crypto.randomUUID(),
                  status: status as any,
                  timestamp: new Date().toISOString(),
                  notes,
                  repairId: id,
                },
              ],
            };
          }
          return repair;
        });
        this.repairs.next(updatedRepairs);
      })
    );
  }

  getRepair(id: string): Repair | undefined {
    return this.repairs.getValue().find((repair) => repair.id === id);
  }
}
