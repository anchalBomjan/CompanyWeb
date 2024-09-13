import { IUser } from "./User";

export class UserParams{
    username:string;
  
    pageNumber=1;
    pageSize=5;
    orderBy ='lastActive';

    constructor(user:IUser){
        
        this.username= user.Username;
    }
}