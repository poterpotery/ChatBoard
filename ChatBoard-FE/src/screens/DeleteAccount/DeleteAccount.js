import { StyleSheet, Text, TouchableOpacity, View } from 'react-native'
import React from 'react'
import Header from '../../components/Header/Header'
import ServerApi from '../../AxiosApi'
import { useToast } from 'react-native-toast-notifications'
import AsyncStorage from '@react-native-async-storage/async-storage'
import { useDispatch } from 'react-redux'
import { setToken } from '../../store/authentication'

const DeleteAccount = ({ navigation }) => {
    const toast = useToast()
    const dispatch = useDispatch()
    const handleCancel = () => {
        navigation.goBack();
    }
    const handleDelete = () => {
        ServerApi.post("/Auth/DeleteAccount").then((response) => {
            console.log("responise", response?.data)
            toast.show('Profile deleted Successfully', {
                type: 'success',
                placement: 'top',
                offset: 300,
            });
            AsyncStorage.removeItem('Id')
            AsyncStorage.removeItem('Email')
            AsyncStorage.removeItem('FirstName')
            AsyncStorage.removeItem('Token')
            AsyncStorage.removeItem('Phone')

            dispatch(setToken(''));
            navigation.navigate("Authentication");
        }).catch((error) => {
            console.log("error", error)
            toast.show('Something wrong', {
                type: 'danger',
                placement: 'top',
                offset: 300,
            });
        })
        // navigation.navigate("Authentication");

    }
    return (
        <>
            <Header />
            <View style={styles.container}>
                <View
                    style={styles.input}
                >
                    <Text style={styles.deleteText}>
                        Are you sure you want to delete your account? This action cannot be undone.
                    </Text>
                </View>
                <View style={styles.buttonContainer}>
                    <TouchableOpacity style={styles.updateButton} onPress={handleDelete} >
                        <Text style={styles.buttonText}>Yes, Save Changes</Text>
                    </TouchableOpacity>
                    <TouchableOpacity style={styles.cancelButton} onPress={handleCancel}>
                        <Text style={styles.buttonText}>No, Cancel</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </>
    )
}

export default DeleteAccount

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        backgroundColor: '#F9FAFB',
        justifyContent: "center",

    },
    title: {
        fontSize: 24,
        fontWeight: 'bold',
        marginBottom: 20,
        textAlign: 'center',
    },
    input: {
        width: "100%",
        color: "black",
        fontSize: 13,
        borderRadius: 17,
        paddingLeft: 10,
        backgroundColor: "#F0F4F8",
        borderWidth: 1,
        borderColor: "#0000001A",
        marginVertical: 10,
        textAlignVertical: "top",
        paddingHorizontal: 20,
        fontFamily: "Ubuntu-Regular",
        justifyContent: "center",
        alignItems: "center",
        paddingVertical: 30
    },
    buttonContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
    },
    updateButton: {
        backgroundColor: '#4CD964',
        padding: 15,
        borderRadius: 100,
        flex: 1,
        marginRight: 10,
        alignItems: 'center',
    },
    cancelButton: {
        backgroundColor: '#D9684C',
        padding: 15,
        borderRadius: 100,
        flex: 1,
        marginLeft: 10,
        alignItems: 'center',
    },
    buttonText: {
        color: 'black',
        fontSize: 12,
        fontFamily: "Ubuntu-Bold"
    },
    deleteText: {
        color: 'black',
        fontSize: 12,
        fontFamily: "Ubuntu-Bold"
    },
})