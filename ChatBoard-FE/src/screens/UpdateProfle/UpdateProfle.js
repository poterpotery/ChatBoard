import { StatusBar, StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native'
import React, { useState } from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import Header from '../../components/Header/Header'
import ProfileSettings from '../../components/Svgs/ProfileSettings'
import DeleteSvg from '../../components/Svgs/DeleteSvg'
import { ACTIVE_OPACITY } from '../../utils/constants'
import { useNavigation, useRoute } from '@react-navigation/native'
import AsyncStorage from '@react-native-async-storage/async-storage'
import { useToast } from 'react-native-toast-notifications'
import ServerApi from '../../AxiosApi'

const UpdateProfile = () => {
    const route = useRoute()
    const toast = useToast()
    const navigation = useNavigation()
    const { PrevEmail, PrevName, PrevPhone } = route.params;
    // State variables for the input fields
    const [name, setName] = useState(PrevName ? PrevName : '')
    const [email, setEmail] = useState(PrevEmail ? PrevEmail : '')
    const [phone, setPhone] = useState(PrevPhone ? PrevPhone : '')

    const handleCancel = () => {
        navigation.goBack();
    }

    const handleUpdate = async () => {
        let body = {
            "firstName": name,
            "lastName": "",
            "gender": 1
        }
        ServerApi.post("/Auth/UpdateAccount", body).then(async (response) => {
            await AsyncStorage.setItem('FirstName', name);
            await AsyncStorage.setItem('Email', email);
            await AsyncStorage.setItem('Phone', phone);
            // Navigate back after logging the data
            toast.show('Profile Updated Successfully', {
                type: 'success',
                placement: 'top',
                offset: 300,
            });
            navigation.goBack();
        }).catch((error) => console.log("error", error));

    }

    return (
        <>
            <StatusBar
                translucent
                backgroundColor="transparent"
                barStyle="dark-content"
            />
            <Header />
            <Text style={styles.header}>Update Profile</Text>

            <View style={styles.container}>
                <View style={styles.card}>
                    <Text style={styles.text}>Name</Text>
                    <TextInput
                        style={styles.textInput}
                        placeholder='Full Name'
                        value={name}  // Bind to the state
                        onChangeText={setName}  // Update state on change
                    />
                </View>
                <View style={styles.card}>
                    <Text style={styles.text}>Email</Text>
                    <TextInput
                        style={styles.textInput}
                        placeholder='testing@gmail.com'
                        value={email}  // Bind to the state
                        onChangeText={setEmail}  // Update state on change
                        editable={false}
                    />
                </View>
                <View style={styles.card}>
                    <Text style={styles.text}>Phone</Text>
                    <TextInput
                        style={styles.textInput}
                        placeholder='xxx-xxxxxxxx'
                        value={phone}  // Bind to the state
                        onChangeText={setPhone}  // Update state on change
                    />
                </View>
            </View>

            <View style={styles.buttonContainer}>
                <TouchableOpacity style={styles.updateButton} onPress={handleUpdate} >
                    <Text style={styles.buttonText}>Save Changes</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.cancelButton} onPress={handleCancel}>
                    <Text style={styles.buttonText}>Cancel</Text>
                </TouchableOpacity>
            </View>
        </>
    )
}

export default UpdateProfile

const styles = StyleSheet.create({
    header: {
        fontSize: 25,
        fontFamily: "Ubuntu-Bold",
        padding: 20,
        color: '#333',
        textAlign: "center"
    },
    container: {
        width: "100%",
        justifyContent: "center",
        alignItems: "center",
    },
    card: {
        height: 60,
        width: "80%",
        borderBottomWidth: 1,
        borderBottomColor: "#878181",
        flexDirection: "row",
        alignItems: "center",
    },
    text: {
        fontFamily: "Ubuntu-Regular",
        marginLeft: 10
    },
    buttonContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        margin: 20
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
    textInput: {
        width: "70%",
        marginLeft: 10,
        fontFamily: "Ubuntu-Regular"
    }
})
