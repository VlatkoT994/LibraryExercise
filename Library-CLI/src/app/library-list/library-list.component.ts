import { ApiService } from './../api.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-library-list',
  templateUrl: './library-list.component.html',
  styleUrls: ['./library-list.component.css'],
})
export class LibraryListComponent implements OnInit {
  libraryList;
  constructor(private api: ApiService) {}
  displayEdit = false;

  testMethod() {
    console.log('works');
  }
  ngOnInit() {
    this.api.getLibraries().subscribe((res) => {
      console.log(res);
      this.libraryList = res;
    });
  }
}
