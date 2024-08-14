import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AdminService } from '../../services/admin.service';

@Component({
  selector: 'app-roles',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './roles.component.html',
  styleUrl: './roles.component.css'
})
export class RolesComponent {
  roles: any[] = [];
  usersWithRoles: any[] = [];
  username: string = '';
  roleName: string = '';
  constructor(private adminService: AdminService) {
    this.getAllRoles();
    this.getAllUsersWithRoles();
  }
  seedRoles() {
    this.adminService.seedRoles().subscribe(
      (response) => {
        console.log(response);
        alert('Roles seeded successfully');
        this.getAllRoles(); // Refresh roles list after seeding
      }
    
    );
  }

  assignRole() {
    this.adminService.assignRoleToUser(this.username, this.roleName).subscribe(
      (response) => {
        console.log(response);
        alert('Role assigned successfully');
        this.getAllUsersWithRoles(); // Refresh users with roles list after assignment
      },
     
    );
  }

  getAllRoles() {
    this.adminService.getAllRoles().subscribe(
      (data) => {
        this.roles = data;
      }
     
    );
  }

  getAllUsersWithRoles() {
    this.adminService.getAllUsersWithRoles().subscribe(
      (data) => {
        this.usersWithRoles = data;
      }
     
    );
  }
  removeRoleFromUser(username: string, roleName: string) {
    this.adminService.removeRoleFromUser(username, roleName).subscribe({
      next: (response) => {
        console.log(response);
        alert('Role removed successfully');
        this.getAllUsersWithRoles(); // Refresh users with roles list after removal
      },
      error: (error) => {
        console.error(error);
        alert('Error removing role');
      },
      complete: () => {
        // Optional: Perform any cleanup or final actions if needed
        console.log('Role removal operation completed.');
      }
    });
  }
  


}
