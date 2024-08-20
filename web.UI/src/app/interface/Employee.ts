export interface IEmployee {
  employeeId: number; // Required field as ID is provided by the user
  name: string;
  email: string;
  phone: string;
  dateOfBirth: string;
  address: string;
  hireDate: string;
  image?: File | null; // For file uploads
  imageBase64?: string; // For displaying base64-encoded image data to retrive the data from  backend
  imageData?: string; // Add this if you need it in your template
  }