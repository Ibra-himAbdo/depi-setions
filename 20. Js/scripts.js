let form = document.querySelector("#task-form");
let taskInput = document.querySelector("#task-input");
let items = document.querySelector(".list-group");

form.addEventListener("submit", function (e) {
  e.preventDefault();

  if (taskInput.value.trim() === "") {
    alert("Cant take empty task");
    return;
  }

  let newLi = document.createElement("li");
  newLi.className = "list-group-item";
  newLi.innerHTML = taskInput.value;
  items.appendChild(newLi);
  taskInput.value = "";
});

items.addEventListener("mouseover", (e) => {
  let item = e.target.closest("li");
  item.classList.add("active");
});

items.addEventListener("mouseout", (e) => {
  let item = e.target.closest("li");
  item.classList.remove("active");
});
