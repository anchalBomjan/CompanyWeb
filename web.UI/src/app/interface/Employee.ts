export interface IEmployee {
    employeeId?: number;
    name: string;
    email: string;
    phone: string;
    

    image?: File; // This will be uploaded as binary data
    dateOfBirth: string;
    address: string;
    hireDate: string;
  }