<template>
    <div>
        <h3>Рецепты, отправленные на модерацию</h3>
    </div>
    <showRecipeDetails :selectRecipe="selectedRecipe"
                       v-if="modalRecipeVisible"
                       @closeModalWindow="closeRecipeModalWindow"
                       @recipePublicationCompleted="recipePublicationCloseModal"
                       @recipeDeletionComplete="recipeDeletionCloseModal"/>
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
                <tr v-for="recipe in unpublishedRecipes" :key="recipe.recipeId" @click="showDetails(recipe)">
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
import AxiosInstance from '@/api_instance';
import showRecipeDetails from '@/components/privilege/partial/ModeratedRecipeDetails.vue';

export default {
    name: 'recipesForModeration',

    components:{
        showRecipeDetails,
    },

    mounted(){
        var vm = this;

            // Метод для получения списка рецептов, которые не прошли модерацию
            AxiosInstance.get("Recipe/GetUnpublishedRecipes")
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
            modalRecipeVisible: false,
            selectedRecipe: null,
        }
    },

    methods: {
        // Обработчик нажатия на строку с рецептом для просмотра деталей
        showDetails(recipe){
            this.selectedRecipe = recipe;
            this.modalRecipeVisible = true;
        },
        closeRecipeModalWindow(){
            this.modalRecipeVisible = false;
        },
        recipePublicationCloseModal(data){
            this.modalRecipeVisible = false;
            console.log('recipePublicationCloseModal DATA: ' + data);
            const index = this.unpublishedRecipes.findIndex(recId => recId.recipeId == data);
            if(index !== -1){
                this.unpublishedRecipes.splice(index,1);
            }
        },
        recipeDeletionCloseModal(data){
            this.modalRecipeVisible = false;
            console.log('recipeDeletionCloseModal DATA: ' + data);
            const index = this.unpublishedRecipes.findIndex(recId => recId.recipeId == data);
            if(index !== -1){
                this.unpublishedRecipes.splice(index,1);
            }
        },
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