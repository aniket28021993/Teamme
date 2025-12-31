

export class LoggedInUser {
    constructor(
        public OrgUserId: number,
        public OrgUserStatusId: number,
        public OrgId: number,
        public OrgStatusId: number,
        public FirstName: string,
        public LastName: string,
        public PhoneNo: string,
        public OrgName: string,
        public Accesstoken: string,
        public EmailId: string,
        public OrgUserTypeId: number,
        public ProfileImage: string,
        public BioData: string,
        public OrgPlanId: number,
        public Breadcrumb: string,
        public IsloggedIn: boolean = false
    ) { }
}
