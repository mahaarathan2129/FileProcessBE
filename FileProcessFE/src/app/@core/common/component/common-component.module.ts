import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './sidebar/sidebar.component'; 
import { NavbarComponent } from './navbar/navbar.component'; 
import { FooterComponent } from './footer/footer.component';
import { NbSidebarModule, NbLayoutModule, NbButtonModule, NbSidebarService, NbCardModule, NbMenuModule,NbMenuService, NbActionsModule, NbInputModule, NbIconModule, NbUserModule, NbContextMenuModule, NbSearchModule, NbDialogModule } from '@nebular/theme';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { ScrollComponent } from './scroll/scroll.component';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    PagenotfoundComponent,
    ScrollComponent

  ],
  imports: [
    CommonModule,
    NbSidebarModule,
    NbLayoutModule,
    NbButtonModule,
    NbMenuModule,
    NbCardModule,
    NbActionsModule,
    NbInputModule,
    NbEvaIconsModule,
    NbIconModule,
    NbUserModule,
    NbContextMenuModule,
    NbSearchModule,
    NbDialogModule.forRoot(),
    ToastrModule

  ],
  exports :[FooterComponent,NavbarComponent,SidebarComponent,ScrollComponent],
  providers: [NbSidebarService,NbMenuService],

})
export class CommonComponentModule { }
