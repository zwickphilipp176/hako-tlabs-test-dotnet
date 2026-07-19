<template>
  <div class="formContainer">
    <div class="createToDoItemContainer">
      <input class="createInput" v-model="newTask" @keyup.enter="addTask" placeholder="Add a new todo" />
      <button class="createButton" @click="addTask">Add</button>
    </div>

    <div class="searchToDoItemContainer">
      <input class="searchInput" v-model="searchTerm" @keyup.enter="searchTasks" placeholder="Search tasks..." />
      <button class="searchButton" @click="searchTasks">Search</button>
    </div>

    <div class="toDoListContainer">
      <div v-for="task in todos" :key="task.id">
        <div class="toDoRow">
          <input type="checkbox" @change="updateTask(task)" v-model="task.isCompleted" />
          <input :class="{ completed: task.isCompleted, editInput: true }" v-model="task.title" @blur="updateTask(task)"
            @keyup.enter="updateTask(task)" />
          <button class="deleteButton" @click="deleteTask(task)">Delete</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, computed, defineComponent } from "vue";
import { useToDoStore } from "../stores/toDoStore.js";

export default defineComponent({
  name: "ToDoList",
  async setup() {
    const todoStore = useToDoStore();
    const todos = computed(() => todoStore.tasks);

    await todoStore.fetchTasks();

    const searchTerm = ref("");

    const searchTasks = () => {      
      todoStore.searchTerm = searchTerm.value.trim();
      todoStore.fetchTasks();
    };

    const newTask = ref("");

    const addTask = () => {
      if (newTask.value.trim()) {
        todoStore.addTask({ title: newTask.value, isCompleted: false });
        newTask.value = "";
      }
    };

    const deleteTask = async (task) => {
      await todoStore.deleteTask(task);
    };

    const updateTask = async (task) => {
      await todoStore.editTask(task);
    };

    return {
      todos,
      newTask,
      addTask,
      deleteTask,
      updateTask,
      searchTerm,
      searchTasks,
    };
  },
});
</script>

<style scoped>
.completed {
  text-decoration: line-through;
}

.toDoListContainer {
  display: flex;
  flex-direction: column;
  gap: 10px;
  margin-top: 20px;
}

.createToDoItemContainer, .searchToDoItemContainer {
  display: flex;
  flex-direction: row;
  gap: 10px;
  margin-bottom: 10px;
}

.formContainer {
  display: flex;
  flex-direction: column;
  margin-left: auto;
  margin-right: auto;
  width: 400px;
}

.createButton, .searchButton, .deleteButton, .updateButton {
  margin-right: auto;
  margin-left: 0;
  width: 60px;
}

.toDoRow {
  display: flex;
  gap: 10px;
}

.createInput, .searchInput {
  flex-grow: 1;
}

.editInput {
  flex-grow: 1;
}
</style>
