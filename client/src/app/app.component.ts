import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
// import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent implements OnInit {
  title = 'Dating App';
  users: any;

  constructor (private http: HttpClient){}

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/users').subscribe(
    {
      next: Response => this.users = Response,
      error: () => console.log(console.error()),
      complete: () => console.log('Request has completed')

    });
    // Subscribre to the obvserable 

    // throw new Error('Method [ngOnInit] not implemented.');
  }

}
