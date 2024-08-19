import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { IDesignation } from '../../interface/Designation';
import { DesignationService } from '../../services/designation.service';

@Component({
  selector: 'app-designation-list',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterModule],
  templateUrl: './designation-list.component.html',
  styleUrl: './designation-list.component.css'
})
export class DesignationListComponent {
  designations: IDesignation[] = [];

  constructor(private designationService: DesignationService, private router: Router) {
    this.loadDesignations();
  }

  loadDesignations(): void {
    this.designationService.getDesignations().subscribe((data: IDesignation[]) => {
      this.designations = data;
    });
  }

 
  delete(id: number): void {
    this.designationService.deleteDesignation(id).subscribe(() => {
      this.designations = this.designations.filter(desig => desig.designationId !== id);
    });
  }

  edit(id: number): void {
    this.router.navigate(['/app-admin/app-designation-form', id]); // Navigate to edit form with designationId
  }
  
  addDesignation(): void {
    this.router.navigate(['/app-admin/app-designation-form']); // Navigate to add form
  }
}
