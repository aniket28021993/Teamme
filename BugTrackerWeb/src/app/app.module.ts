
//ANGULAR
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppAsideModule,AppBreadcrumbModule,AppHeaderModule,AppFooterModule,AppSidebarModule } from '@coreui/angular';
import { LocalStorageModule } from 'angular-2-local-storage';


//ANGULAR MATERIAL
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CKEditorModule } from 'ckeditor4-angular';
import { MatTabsModule } from '@angular/material/tabs';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';

//PRIMENG
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { PanelModule } from 'primeng/panel';
import { OrganizationChartModule } from 'primeng/organizationchart';
import { ChartModule } from 'primeng/chart';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { FileUploadModule } from 'primeng/fileupload';
import { DialogModule } from 'primeng/dialog';
import { CalendarModule } from 'primeng/calendar';
import { FullCalendarModule } from 'primeng/fullcalendar';


//3RD PARTY MODUEL
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ChartsModule } from 'ng2-charts';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { NgHttpLoaderModule } from 'ng-http-loader';
import { MentionModule } from 'angular-mentions';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {suppressScrollX: true};
const APP_CONTAINERS = [DefaultLayoutComponent];


//ROUTING
import { AppRoutingModule } from './app.routing';


//SERVICE
import { MiscellaneousService } from '../app/miscellaneous/miscellaneous.service';
import { AdminService } from './miscellaneous/Admin.service';
import { TokenInterceptorService } from './miscellaneous/Interceptor.service';
import { DashboardService } from './miscellaneous/dashboard.service';
import { APIUrl } from './appsetting';


//COMPONENT
import { AppComponent } from './app.component';
import { SpinnerComponent } from './spinner.component';
import { DefaultLayoutComponent } from './containers';
import { CommentComponent } from './client/comment/comment.component';
import { UserProfileComponent } from './client/userprofile/userprofile.component';
import { ChangePasswordComponent } from './client/changepassword/changepassword.component';
import { RecentActivityComponent } from './client/recentactivity/recentactivity.component';


import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { ForgotPasswordComponent } from './views/forgotpassword/forgotpassword.component';

import { AdminDashboardComponent } from './admin/admindashboard.component';
import { OrgUsersComponent } from './admin/orgusers.component';
import { ClientDashboardComponent } from './client/clientdashboard/clientdashboard.component';
import { MainDashboardComponent } from './client/clientdashboard/maindashboard.component';

import { UsersComponent } from './client/users/users.component';
import { PhaseComponent } from './client/phase/phase.component';

import { ProductBacklogComponent } from './client/productbacklog/productbacklog.component';
import { CreateProductComponent } from './client/productbacklog/createproduct.component';
import { EditProductComponent } from './client/productbacklog/editproduct.component';

import { ProductTaskComponent } from './client/producttask/producttask.component';
import { CreateTaskComponent } from './client/producttask/createtask.component';
import { EditTaskComponent } from './client/producttask/edittask.component';

import { ProductEnhancementComponent } from './client/productenhancement/productenhancement.component';
import { CreateenhancementComponent } from './client/productenhancement/createenhancement.component';
import { EditEnhancementComponent } from './client/productenhancement/editenhancement.component';

import { ProductBugComponent } from './client/productbug/productbug.component';
import { CreateBugComponent } from './client/productbug/createbug.component';
import { EditBugComponent } from './client/productbug/editbug.component';

import { TestCaseComponent } from './client/testcase/testcase.component';
import { CreateTestCaseComponent } from './client/testcase/createtestcase.component';
import { EditTestCaseComponent } from './client/testcase/edittestcase.component';

import { PlanComponent } from './client/plan/plan.component';
import { OrgPlanComponent } from './admin/orgplan.component';
import { PaymentComponent } from './admin/payment.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { ThankYouComponent } from './views/thankyou/thankyou.component';

import { TourMatMenuModule } from 'ngx-tour-md-menu';
import { ErrorInterceptor } from './miscellaneous/errorinterceptor.service';
import { ScrumBoardComponent } from './client/scrumboard/scrumboard.component';
import { ProjectService } from './miscellaneous/project.service';
import { PhaseService } from './miscellaneous/phase.service';
import { BacklogService } from './miscellaneous/backlog.service';
import { TaskService } from './miscellaneous/task.service';
import { EnhancementService } from './miscellaneous/enhancement.service';
import { BugService } from './miscellaneous/bug.service';
import { TestCaseService } from './miscellaneous/testcase.service';
import { UserService } from './miscellaneous/user.service';
import { GenericService } from './miscellaneous/generic.service';
import { PrivacyPolicyComponent } from './views/policy/privacypolicy.component';
import { TermOfServiceComponent } from './views/policy/termofservice.component';
import { DailyTaskComponent } from './client/dailytask/dailytask.component';

@NgModule({
  imports: [
    LocalStorageModule.forRoot({
      prefix: 'my-app',
      storageType: 'localStorage'
    }),
    NgHttpLoaderModule.forRoot(),
    ToastModule,
    MessageModule,
    DialogModule,
    MessagesModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSelectModule,
    OrganizationChartModule,
    MatRadioModule,
    ChartModule,
    MatSortModule,
    PanelModule,
    BrowserAnimationsModule,
    NgMultiSelectDropDownModule.forRoot(),
    TourMatMenuModule.forRoot(),
    ReactiveFormsModule,
    BrowserModule,
    CommonModule,
    RouterModule,
    AppRoutingModule,
    AppAsideModule,
    HttpClientModule,
    MatProgressSpinnerModule,
    AppBreadcrumbModule.forRoot(),
    AppFooterModule,
    AppHeaderModule,
    AppSidebarModule,
    PerfectScrollbarModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ChartsModule,
    CardModule,
    FileUploadModule,
    ButtonModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    CalendarModule,
    CKEditorModule,
    FullCalendarModule,
    MatTabsModule,
    MentionModule,
    MatCheckboxModule,
    MatTabsModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
  ],
  declarations: [
    AppComponent,
    ...APP_CONTAINERS,
    P404Component,
    P500Component,
    LoginComponent,
    RegisterComponent,
    AdminDashboardComponent,
    ClientDashboardComponent,
    UsersComponent,
    PhaseComponent,
    ProductBacklogComponent,
    ProductTaskComponent,
    ProductEnhancementComponent,
    ProductBugComponent,
    CreateProductComponent,
    EditProductComponent,
    CreateTaskComponent,
    EditTaskComponent,
    CreateenhancementComponent,
    EditEnhancementComponent,
    CreateBugComponent,
    EditBugComponent,
    SpinnerComponent,
    CommentComponent,
    UserProfileComponent,
    ChangePasswordComponent,
    RecentActivityComponent,
    TestCaseComponent,
    CreateTestCaseComponent,
    EditTestCaseComponent,
    OrgUsersComponent,
    ForgotPasswordComponent,
    PlanComponent,
    OrgPlanComponent,
    PaymentComponent,
    ThankYouComponent,
    MainDashboardComponent,
    ScrumBoardComponent,
    TermOfServiceComponent,
    PrivacyPolicyComponent,
    DailyTaskComponent
  ],
  entryComponents: [SpinnerComponent],
  providers:
    [{ provide: LocationStrategy, useClass: HashLocationStrategy},
      { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true },
      { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    MiscellaneousService,
      AdminService,
      DashboardService,
      ProjectService,
      PhaseService,
      BacklogService,
      TaskService,
      EnhancementService,
      BugService,
      TestCaseService,
      UserService,
      GenericService,
      MessageService,
      APIUrl],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
