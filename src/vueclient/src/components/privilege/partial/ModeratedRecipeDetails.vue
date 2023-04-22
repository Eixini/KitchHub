<template>
    <div class="container">
        <div class="modal-window">
            <div class="modal-header">
                <p>Модерация рецепта</p>
                <button type="button" @click="closeModal">X</button>
            </div>
            <div class="modal-content">
                <div class="modal-content-title">{{ selectRecipe.name }}</div>
                <div class="modal-content-nickname"> {{ selectRecipe.nickName }}</div>
                <div class="modal-content-type"> {{ selectRecipe.type }}</div>
                <div class="modal-content-national"> {{ selectRecipe.national }}</div>
                <div class="modal-content-ingredients"></div>
                <div class="modal-content-description"> {{ selectRecipe.description }}</div>
            </div>
            <div class="model-footer">
                <button type="button" @click="approveRecipe">Опубликовать</button>
                <button type="button" @click="rejectRecipe">Удалить</button>
            </div>
        </div>
    </div>
</template>

<script lang="js">
import AxiosInstance from '@/api_instance';

export default {

    props: {
        selectRecipe: {},
        recipeId: Number,
    },

    mounted(){
        console.log('Recipe: ' + this.selectRecipe);
        console.log('Recipe id: ' + this.selectRecipe.recipeId);
        //console.log('Recipe name: ' + selectRecipe.name);
    },

    data() {
        return {

        }
    },

    methods: {
        closeModal(){
            this.$emit('closeModalWindow');
        },

        approveRecipe(){
            console.log('Approve recipe id: ' + this.selectRecipe.recipeId);
            AxiosInstance.post('Recipe/PublishRecipe', {
                recipeId: this.selectRecipe.recipeId
                },)
                .then(function (response) {
                    alert(response.data);
                    this.$emit('closeModalWindow');
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        rejectRecipe(){
            console.log('Approve delete id: ' + this.selectRecipe.recipeId);
            AxiosInstance.delete('Recipe/DeleteUnpublishedRecipes', {
                recipeId: this.selectRecipe.recipeId
                },)
                .then(function (response) {
                    alert(response.data);
                    this.$emit('closeModalWindow');
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    },

}
</script>

<style scoped>
.modal-window{
    position: fixed;
    box-shadow: 0 0 15px 0;
    background: #ffffff;
}
</style>