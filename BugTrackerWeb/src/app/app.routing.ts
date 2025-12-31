
//ANGULAR
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//COMPONENT
import { DefaultLayoutComponent } from './containers';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';

import { AdminDashboardComponent } from './admin/admindashboard.component';
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

import { UserProfileComponent } from './client/userprofile/userprofile.component';
import { ChangePasswordComponent } from './client/changepassword/changepassword.component';
import { RecentActivityComponent } from './client/recentactivity/recentactivity.component';
import { OrgUsersComponent } from './admin/orgusers.component';
import { ForgotPasswordComponent } from './views/forgotpassword/forgotpassword.component';
import { PlanComponent } from './client/plan/plan.component';
import { OrgPlanComponent } from './admin/orgplan.component';
import { PaymentComponent } from './admin/payment.component';
import { ThankYouComponent } from './views/thankyou/thankyou.component';
import { ScrumBoardComponent } from './client/scrumboard/scrumboard.component';
import { PrivacyPolicyComponent } from './views/policy/privacypolicy.component';
import { TermOfServiceComponent } from './views/policy/termofservice.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'register',
    pathMatch: 'full',
  },
  {
    path: '404',
    component: P404Component,
    data: {
      title: 'Page 404'
    }
  },
  {
    path: '500',
    component: P500Component,
    data: {
      title: 'Page 500'
    }
  },
  {
    path: 'thankyou',
    component: ThankYouComponent
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },
  {
    path: 'forgotpassword',
    component: ForgotPasswordComponent
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {
      title: 'Register Page'
    }
  },
  {
    path: 'privacypolicy',
    component: PrivacyPolicyComponent,
  },
  {
    path: 'termofservice',
    component: TermOfServiceComponent,
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'plan',
        component: PlanComponent
      },
      {
        path: 'scrumboard',
        component: ScrumBoardComponent
      },
      {
        path: 'maindashboard',
        component: MainDashboardComponent
      },
      {
        path: 'orgplan',
        component: OrgPlanComponent
      },
      {
        path: 'payment',
        component: PaymentComponent
      },
      {
        path: 'admin-dashboard',
        component: AdminDashboardComponent
      },
      {
        path: 'client-dashboard',
        component: ClientDashboardComponent
      },
      {
        path: 'users',
        component: UsersComponent
      },
      {
        path: 'orguser/:OrgId',
        component: OrgUsersComponent
      },
      {
        path: 'phase',
        component: PhaseComponent
      },
      {
        path: 'userprofile',
        component: UserProfileComponent
      },
      {
        path: 'changepassword',
        component: ChangePasswordComponent
      },
      {
        path: 'recentactivity/:CommonId/:RecentActivityId',
        component: RecentActivityComponent
      },
    ]
  },
  {
    path: 'backlog',
    component: DefaultLayoutComponent,
    children: [{
      path: '',
      component: ProductBacklogComponent
    },
    {
        path: 'create/:PhaseId/:ProjectId',
        component: CreateProductComponent
    },
    {
        path: 'edit/:ProductBacklogId/:ProjectId',
        component: EditProductComponent
    }]
  },
  {
    path: 'task',
    component: DefaultLayoutComponent,
    children: [{
      path: '',
      component: ProductTaskComponent
    },
    {
        path: 'create/:ProductBacklogId/:PhaseId/:ProjectId',
        component: CreateTaskComponent
    },
    {
        path: 'edit/:ProductBacklogDataId/:ProjectId',
        component: EditTaskComponent
    }]
  },
  {
    path: 'enhancement',
    component: DefaultLayoutComponent,
    children: [{
      path: '',
      component: ProductEnhancementComponent
    },
    {
        path: 'create/:ProductBacklogId/:PhaseId/:ProjectId',
        component: CreateenhancementComponent
    },
    {
        path: 'edit/:ProductBacklogDataId/:ProjectId',
        component: EditEnhancementComponent
    }]
  },
  {
    path: 'bug',
    component: DefaultLayoutComponent,
    children: [{
      path: '',
      component: ProductBugComponent
    },
    {
        path: 'create/:ProductBacklogId/:PhaseId/:ProjectId',
        component: CreateBugComponent
    },
    {
        path: 'edit/:ProductBacklogDataId/:ProjectId',
        component: EditBugComponent
    }]
  },
  {
    path: 'testcase',
    component: DefaultLayoutComponent,
    children: [{
      path: '',
      component: TestCaseComponent
    },
    {
      path: 'create/:ProductBacklogId/:PhaseId/:ProjectId',
      component: CreateTestCaseComponent
    },
    {
      path: 'edit/:ProductBacklogDataId/:ProjectId',
      component: EditTestCaseComponent
    }]
  },
  { path: '**', component: P404Component }
];
@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
