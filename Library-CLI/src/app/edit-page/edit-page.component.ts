import { PatchDoc, ForEdit } from './../models/edit';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-edit-page',
  templateUrl: './edit-page.component.html',
  styleUrls: ['./edit-page.component.css'],
})
export class EditPageComponent implements OnInit {
  @Input() objectInfo: ForEdit[];
  @Input() objectId: number;
  @Input() patchRequest: any;
  editingData: ForEdit[];

  constructor() {}

  ngOnInit(): void {
    this.editingData = JSON.parse(JSON.stringify(this.objectInfo));
    console.log(this.editingData);
  }

  patchAll() {
    let patchDoc = [];
    this.editingData.map((value) => {
      patchDoc.push(new PatchDoc(value.key, value.value));
    });
    this.patchRequest(this.objectId, patchDoc);
  }
  patchSingle(i) {
    let patchDoc = [
      new PatchDoc(this.editingData[i].key, this.editingData[i].value),
    ];
    console.log(patchDoc);
    this.patchRequest(this.objectId, patchDoc).subscribe((res) =>
      console.log(res)
    );
  }
}
