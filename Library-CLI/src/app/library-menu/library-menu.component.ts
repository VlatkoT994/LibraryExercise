import { ApiService } from './../api.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-library-menu',
  templateUrl: './library-menu.component.html',
  styleUrls: ['./library-menu.component.css'],
})
export class LibraryMenuComponent implements OnInit {
  id: number;
  library: object;
  constructor(private route: ActivatedRoute, private api: ApiService) {}

  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id');
    this.api.getLibrary(this.id).subscribe((r) => console.log(r));
  }
}
