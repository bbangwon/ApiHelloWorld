console.log("NoteAPI REST API Test");

//[0] Web API URL
var url = "https://localhost:7274/api/NoteService";

//[1] Fetch API 사용하기
console.log(fetch(url));

//[2] Fetch API 결괏값 사용
fetch(url)
    .then(response => response.json())
    .then(data => console.log(data));

//[3] Fetch API로 데이터 전송
fetch(url, {
    method: "POST",
    headers: {
        "Content-Type": "application/json"
    },
    body: JSON.stringify({
        content: 'FetchTest'
    })
}).then(response => {
    if (response.ok) {
        console.log("성공");
        return response.json();
    }
    else {
        console.log("에러");
    }
}).then(data => console.log(data));
