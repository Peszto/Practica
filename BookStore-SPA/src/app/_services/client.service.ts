import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Client } from "../_models/Client"
import { Observable } from "rxjs";
import { BasicModel } from "../_models/BasicModel";
import { ApiResponse } from "../_models/ApiResponse";

@Injectable({
    providedIn:'root'
})

export class ClientService{
    private baseUrl :string = environment.baseUrl+'api/';

    constructor(private http:HttpClient){}

    public addClient(client : Client){
        return this.http.post(this.baseUrl+'clients',client);
    }

    public updateClient(id:number,client:Client){
        return this.http.put(this.baseUrl+'clients/'+id,client);
    }

    public getClients():Observable<Client[]>{
        return this.http.get<Client[]>(this.baseUrl+'clients');
    }

    public getClientById(id: any): Observable<Client>{
        return this.http.get<Client>(this.baseUrl + 'clients/' + id);
    } 

    public deleteClient(id:number){
        return this.http.delete(this.baseUrl+'clients/'+id);
    }

    public filterClientNames(filteredValue : string): Observable<BasicModel[] | ApiResponse> {
        return this.http.get<BasicModel[] | ApiResponse>(this.baseUrl+'clients/filter/' + filteredValue);
    }
}