class StudentDetail {
    constructor(studId,firstName,lastName,dob,contactNo) {
        this.studId=studId?studId:0;
      this.firstName = brand;
      this.lastName=lastName;
      this.dob=dob;
      this.contactNo=contactNo;
    }
    printObject() {
      return JSON(this);
    }
  }