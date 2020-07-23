import { ForEdit, PatchDoc } from './../models/edit';
import { ApiService } from './../api.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-library-menu',
  templateUrl: './library-menu.component.html',
  styleUrls: ['./library-menu.component.css'],
})
export class LibraryMenuComponent implements OnInit {
  id: string;
  library: object;
  showEditDialog = false;
  dataForEdit: ForEdit[];
  patchRequest: any;

  constructor(private route: ActivatedRoute, private api: ApiService) {}

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      this.id = params.get('id');
      this.api.getLibrary(this.id).subscribe((res) => {
        console.log(res);
        this.library = res;
      });
    });
  }

  startEditing() {
    this.showEditDialog = true;
    this.dataForEdit = [
      new ForEdit('name', 'string', this.library.name),
      new ForEdit('city', 'string', this.library.city),
      new ForEdit('address', 'string', this.library.address),
    ];
    this.patchRequest = (id, patchDoc) => {
      return this.api.patchLibrary(id, patchDoc);
    };
  }
  deleteLibrary() {
    console.log('delete');
    this.api.deleteLibrary(this.id).subscribe((res) => {
      console.log('succesfully deleted');
    });
  }
}
