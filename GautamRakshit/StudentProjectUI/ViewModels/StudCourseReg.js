class StudCourseReg {
    constructor(regId,studname,courseName) {
        this.regID=regId?regId:0;
      this.studName = studname;
      this.courseName=courseName;
    }
    printObject() {
      return JSON(this);
    }

  }