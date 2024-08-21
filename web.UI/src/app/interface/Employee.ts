export interface IEmployee {
  employeeId?: number; // Optional if not provided during creation
  name: string;
  email: string;
  phone: string;
  dateOfBirth: string; // Ensure this format is consistent with the backend
  address: string;
  hireDate: string; // Ensure this format is consistent with the backend
  image?: File | null; // For file uploads
  imageUrl?: string; // To store the image URL returned from the backend
  imageBase64?: string; // For displaying base64-encoded image data
}
