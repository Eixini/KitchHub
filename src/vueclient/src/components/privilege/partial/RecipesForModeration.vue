<template>
    <div>
        <h3>Рецепты, отправленные на модерацию</h3>
    </div>
    <div>
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Recipe Name</th>
                    <th>NationalKitch</th>
                    <th>DishType</th>
                    <th>Who Added</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="recipe,index in unpublishedRecipes" :key="recipe.recipeId" @click="showDetails(index)">
                    <td>{{ recipe.recipeId }}</td>
                    <td>{{ recipe.name }}</td>
                    <td>{{ recipe.national }}</td>
                    <td>{{ recipe.type }}</td>
                    <td>{{ recipe.nickName }}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="js">
import axios from 'axios';

export default {
    name: 'recipesForModeration',

    mounted(){
        var vm = this;
            // Метод для получения списка рецептов, которые не прошли модерацию
            axios.get("https://localhost:5192/Recipe/GetUnpublishedRecipes")
                 .then(function(response){
                    vm.unpublishedRecipes = response.data;
                    console.log(vm.unpublishedRecipes);
                 })
                 .catch(function (error) {
                    console.log(error);
                 });
    },

    data() {
        return {
            unpublishedRecipes: [],
        }
    },

    methods: {
        // Обработчик нажатия на строку с рецептом для просмотра деталей
        showDetails(index){
            alert('Recipe index ' + index);
        }
    }
}
</script>

<style scoped>
table {
   /*Удаление пустых промежутков между ячейками*/
   border-collapse: collapse;
   /*Установка для таблицы внешней границы серого цвета толщиной 1px*/
   border: 1px solid grey;
}

th {
   border: 1px solid grey;
}

td {
   border: 1px solid grey;
}
</style>