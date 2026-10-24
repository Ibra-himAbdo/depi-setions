let newTaskInput = document.querySelector("#add-new-task");
let addForm = document.querySelector("#add-form");
let todoItemsList = document.querySelector("#todo-items");

addForm.addEventListener("submit", (e) => {
  e.preventDefault();
  let text = newTaskInput.value.trim();
  if (text == "") {
    alert("Please enter a task");
    return;
  }
  createNewItem(text);
  newTaskInput.value = "";
});

function createNewItem(text) {
  let newItem = document.createElement("li");
  newItem.className = "list-group-item d-flex gap-1 justify-content-between";
  newItem.innerHTML = `
   <span></span>
    <div class="d-none">
        <button class="btn btn-outline-success btn-sm done-btn">
        Done
        </button>
        <button class="btn btn-outline-warning btn-sm edit-btn">
        Edit
        </button>
        <button class="btn btn-outline-danger btn-sm delete-btn">
        Delete
        </button>`;

  let taskText = newItem.querySelector("span");
  taskText.textContent = text;

  let editBtn = newItem.querySelector(".edit-btn");
  editBtn.addEventListener("click", () => {
    let newItemText = window.prompt("Enter Text: ", taskText.textContent);
    if (newItemText != null) {
      taskText.textContent = newItemText;
    }
  });

  let doneBtn = newItem.querySelector(".done-btn");
  doneBtn.addEventListener("click", () => {
    taskText.classList.toggle("text-decoration-line-through");

    if (taskText.classList.contains("text-decoration-line-through")) {
      doneBtn.textContent = "Un Done";
    } else {
      doneBtn.textContent = "Done";
    }
  });

  todoItemsList.appendChild(newItem);
}

todoItemsList.addEventListener("mouseover", (e) => {
  let selectedItem = e.target.closest("li");
  let taskText = selectedItem.querySelector("span");

  selectedItem.classList.add("active");
  let btnContainer = selectedItem.querySelector("div");
  btnContainer.classList.remove("d-none");

  let deleteBtn = selectedItem.querySelector(".delete-btn");
  deleteBtn.addEventListener("click", () => {
    selectedItem.remove();
  });
});

todoItemsList.addEventListener("mouseout", (e) => {
  let selectedItem = e.target.closest("li");

  selectedItem.classList.remove("active");

  let btnContainer = selectedItem.querySelector("div");
  btnContainer.classList.add("d-none");
});
