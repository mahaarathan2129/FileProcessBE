import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { GlobalVariablesService } from 'src/app/services/global-variables.service';
import { GlobalService } from 'src/app/services/global.service';

@Component({
  selector: 'app-file-process',
  templateUrl: './file-process.component.html',
  styleUrls: ['./file-process.component.scss']
})
export class FileProcessComponent{
  name: string = '';
  isEditMode: boolean = false; 
  editUserId: number | null = null;
  data: any[] = [];
  page: number = 1;
  itemsPerPage: number = 5;

  constructor(private http:HttpClient,private file:GlobalVariablesService,private global:GlobalService,private toastr:ToastrService) {}

  ngOnInit() {
    this.loadTable();
  }

  loadTable(){
    this.http.get(this.file.FILE_URL).subscribe((res:any)=>{
      this.data = res.data
      console.log(res)
    })
  }

  onSubmit() {
    if (this.name) {
        this.http.post(`${this.file.FILE_URL}?filepath=${this.name}`, {}).subscribe(
            (res: any) => {
              this.toastr.success('File Process Initiated...');
              this.loadTable();
            },
            (error) => {
                console.error("Error:", error);
                this.toastr.error(error.error.message);
            }
        );
        this.clearForm();
    } else {
      this.toastr.warning('Please provide a valid file path');
    }
}

  clearForm() {
    this.name = '';
    this.isEditMode = false;
    this.editUserId = null;
  }
}
