class Course {
    constructor(courseId,name,detail) {
        this.courseId=courseId?courseId:0;
      this.name = name;
      this.detail=detail;
    }
    printObject() {
      return JSON(this);
    }
  }