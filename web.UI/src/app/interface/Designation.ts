export interface IDesignation {
    designationId?: number; // Optional if not always provided
    title: string;
    salary: number;
    description: string;
    departmentId: number; // Assuming this is required
  }