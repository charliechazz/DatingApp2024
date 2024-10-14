import { Component } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  standalone: true,
  imports: [],
  templateUrl: './test-errors.component.html',
  styleUrl: './test-errors.component.css'
})
export class TestErrorsComponent {
  baseUrl = "https://localhost:5001/api/";
  private http = inject(HttpClient);

  get400Error(): void {
    this.http.get(this.baseUrl + "buggy/bad-request").subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error)
    })
  }

  get401Error(): void {
    this.http.get(this.baseUrl + "buggy/auth").subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error)
    })
  }

  get404Error(): void {
    this.http.get(this.baseUrl + "buggy/").subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error)
    })
  }

  get500Error(): void {
    this.http.get(this.baseUrl + "buggy/server-error").subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error)
    })
  }

  get400ValidationError(): void {
    this.http.get(this.baseUrl + "account/register", {}).subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error)
    })
  }
  
}
