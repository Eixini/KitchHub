<template>
    <div class="container">
        <div class="modal-window">
            <div class="modal-header">
                <span>Модерация рецепта</span>
                <button type="button"
                        @click="closeModal">
                        X
                </button>
            </div>
            <div class="modal-content">
                <div class="modal-content-title">
                    <span><b>Название блюда:</b></span>
                    {{ selectRecipe.name }}
                </div>
                <div class="modal-content-nickname">
                    <span><b>Отправитель:</b></span>
                    {{ selectRecipe.nickName }}
                </div>
                <div class="modal-content-type">
                    <span><b>Тип блюда:</b></span>
                    {{ selectRecipe.type }}
                </div>
                <div class="modal-content-national">
                    <span><b>Национальность:</b></span>
                    {{ selectRecipe.national }}
                </div>
                <div class="modal-content-ingredients">
                    <span><b>Игредиенты:</b></span>
                    <span class="ingredients-line" v-for="ing in selectRecipe.ingredients" 
                          v-bind:key="ing.name">
                          <button class="ingredient-element">{{ ing.name }}</button>   
                    </span>
                </div>
                <div class="modal-content-description">
                    <p><b>Описание:</b></p>
                    {{ selectRecipe.description }}
                </div>
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
            console.log('Approve recipe id: ' + Number(this.selectRecipe.recipeId));
            AxiosInstance.post('Recipe/PublishRecipe', {
                Id: Number(this.selectRecipe.recipeId)
                },)
                .then((response) => {
                    alert(response.data);
                    this.$emit('recipePublicationCompleted',this.selectRecipe.recipeId);
                })
                .catch((error) => {
                    console.log(error);
                    alert(error);
                });
            
        },

        rejectRecipe(){
            console.log('Approve delete id: ' + Number(this.selectRecipe.recipeId));
            AxiosInstance.delete('Recipe/DeleteUnpublishedRecipes', {
                data: {Id: Number(this.selectRecipe.recipeId)}
                },)
                .then((response) => {
                    alert('Recipe deleted');
                    this.$emit('recipeDeletionComplete',this.selectRecipe.recipeId);
                })
                .catch((error) => {
                    console.log(error);
                    alert(error);
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

.modal-header{
    display: inline-block;
    vertical-align: top;
}

.modal-content-ingredients{
    border: 1px;
    border-color: black;
    display: inline-block;
}

.ingredients-line{
    margin: 5;
}
</style>