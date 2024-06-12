import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-infoview',
  templateUrl: './infoview.component.html',
  styleUrls: ['./infoview.component.scss']
})
export class InfoviewComponent implements OnInit {
  public info: any;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
    this.info = data
    console.log("info", this.info)
   }

  ngOnInit(): void {
  }
}
