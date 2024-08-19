import { IDepartment } from "./Department";
import { IDesignation } from "./Designation";

export interface IDepartmentWithDesignations extends IDepartment {
    designations: IDesignation[];
  }