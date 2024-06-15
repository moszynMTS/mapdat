import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

class FormatedInfo {
  topic: any;
  name: any;
  values: any[];

  constructor(topic: any, values: any[]) {
    this.topic = topic;
    this.values = values;
  }
}

@Component({
  selector: 'app-infoview',
  templateUrl: './infoview.component.html',
  styleUrls: ['./infoview.component.scss']
})
export class InfoviewComponent implements OnInit {
  public info: any;
  public formatedInfo: FormatedInfo[] = [];
  viewVBC: [number, number] = [900, 400];
  animationsVBC = false;
  legendVBC = false;
  xAxisVBC = true;
  yAxisVBC = true;
  showYAxisLabelVBC = true;

  public subjects: any[] = [
    { id: 1, name: "Dochody powiatów według województwa", value: "DOCHODY" },
    { id: 2, name: "Wydatki powiatów według województwa", value: "WYDATKI" },
    { id: 3, name: "Ludność według województw", value: "LUDNOSC" },
    { id: 4, name: "Mediana wieku według województw", value: "MEDIANA WIEKU" },
    { id: 5, name: "Przestępstwa według województw", value: "PRZESTEPSTWA" },
    { id: 6, name: "Biblioteki publiczne według województw", value: "BIBLIOTEKI PUBLICZNE" },
    { id: 7, name: "Kina według województw", value: "KINA" },
    { id: 8, name: "Kluby sportowe według województw", value: "KLUBY SPORTOWE" },
    { id: 9, name: "Gastronomia według województw", value: "GASTRONOMIA" },
    { id: 10, name: "Szpitale według województw", value: "SZPITALE" },
    { id: 11, name: "Żłobki według województw", value: "ZLOBKI" },
    { id: 12, name: "Pracujący według województw", value: "PRACUJACY" },
    { id: 13, name: "Stopa bezrobocia według województw", value: "STOPA BEZROBOCIA" },
    { id: 14, name: "Szkoły według podziału administracyjnego", value: "SZKOLY" }
];
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
    this.info = data;
    this.mapResults()
   }

  ngOnInit(): void {
  }
  getSubjectName(value: string): string {
    const subject = this.subjects.find(subject => subject.value === value);
    return subject ? subject.name : value;
  }
    //topic: Rozwody
  //[wojId: value, ]
  mapResults(): void {
    this.subjects.forEach(element => {
        var topic = element.value;
        var id;
        var values: any[] = []
        this.info.forEach((info: any) => {
          var data = info.data.find((x: any) => x.subject === topic);
          if (data) {
            id = info.wojewodztwoId != null ? info.wojewodztwoId :
                 info.powiatId != null ? info.powiatId :
                 info.gminaId;
            let count = parseFloat(data.count.replace(',', '.'));
            values.push({ id, name: info.name, value: count });
          }
        });
        if(values.length != 0){
          this.formatedInfo.push(new FormatedInfo(topic, values));
        }
    });
  }
}
