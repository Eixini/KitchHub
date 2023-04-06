<template>
    <Form @submit="handleLogin" :validation-schema="schema">
        <div>
            <label for="email">E-mail</label>
            <Field name="email" type="text"/>
            <ErrorMessage name="email"/>
        </div>

        <div>
            <label for="password">Password</label>
            <Field name="password" type="text"/>
            <ErrorMessage name="password"/>
        </div>

        <div>
            <button :disabled="loading">
                <span v-show="loading"></span>
                <span>Login</span>
            </button>
        </div>

        <div>
            <div v-if="message" role="alert">
            {{ message }}
            </div>
        </div>
    </Form>
</template>

<script lang="js">

import { store } from "@/store";
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";

export default {
    name: "Login",
    components: {
        Form,
        Field,
        ErrorMessage
    },

    data() {
        const schema = yup.object().shape({
            email: yup.string().required("E-mail is required!"),
            password: yup.string().required("Password is required!")
        });

        return {
            loading: false,
            message: "",
            schema
        }
    },

    computed: {
        loggedIn() {
            return this.$store.state.auth.status.loggedIn;
        },
    },
    // Если статус верен, используется Vue Router, чтобы направить пользователя на страницу профиля:
    created() {
        if (this.loggedIn) {
            this.$router.push("/profile");
        }
    },

    methods: {
        handleLogin(user) {
            this.loading = true;

            console.log(user);

            store.dispatch("auth/login", user).then(
                () => {
                    this.$router.push('/profile');
                },
                (error) => {
                    this.loading = false;
                    this.message = (error.response &&
                                    error.response.data &&
                                    error.response.data.message) ||
                                   error.message ||
                                   error.toString();
                }
            )
        }
    }

};

</script>