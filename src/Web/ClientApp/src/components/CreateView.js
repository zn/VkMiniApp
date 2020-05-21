import React, {Component} from 'react'
import {View, Panel, PanelHeader, FormLayout, Textarea, File, Checkbox, Button, Link, Gallery, FormLayoutGroup} from '@vkontakte/vkui'
import Icon24Camera from '@vkontakte/icons/dist/24/camera';

class CreateView extends Component{

    state = {
        buttonEnabled: true,
        imagesForPreview:[]
    }
    
    checkboxChanged(e){
        this.setState({buttonEnabled:!e.target.checked});
    }

    filesSelected(input){

        if(input.target.files.length > 5){
            alert("Нельзя приложить больше пяти файлов к посту.");
            input.target.value='';
            return;
        }
        const fileMaxSize = 10485760; // same as in appsettings.json on server
        for(var i=0;i<input.target.files.length;i++){
            if(input.target.files[i].size > fileMaxSize){
                alert(`Файл ${input.target.files[i].name} превышает максимально допустимый размер.`)
                input.target.value='';
                return;
            }
        }

        const gallery = document.getElementById("gallery");
        gallery.innerHTML = ''; // clean up previous images

        if (input.target.files) {
            [].forEach.call(input.target.files, this.readAndPreview.bind(this));
            gallery.style.display = "block";
        }
        else {
            gallery.style.display = "none";
        }
    }

    readAndPreview(file) {
        var reader = new FileReader();
        reader.addEventListener("load", function () {
            this.setState({// и тут this
                imagesForPreview: [...this.state.imagesForPreview, this.result]// и тут
            });
        });
        reader.readAsDataURL(file);
    }
    handleSubmit(event) {
        event.preventDefault();

        // хз где ошибка на сервере или на клиенте. удачи
        fetch('/posts', {
            method: 'POST',
            body: new FormData(event.target)
        });
      }


    render(){
        const {id, fetchedUserId} = this.props;
        return (
            <View id={id} activePanel="create">
                <Panel id="create">
                    <PanelHeader>Создать объявление</PanelHeader>

                    <FormLayout id="myform" onSubmit={this.handleSubmit.bind(this)}>
                        <Textarea top="Текст сообщения" 
                                name="content"
                                placeholder="Введите текст"/>

                        <File name="attachments"
                            accept="image/png, image/jpeg"
                            multiple
                            top="Добавить фото"
                            before={<Icon24Camera/>} size="l"
                            onChange={this.filesSelected.bind(this)} >
                            Открыть галерею
                        </File>
                        
                        <Gallery id="gallery"
                            slideWidth="90%"
                            style={{ height: "max-content", display:"none" }}
                            bullets="dark">
                            {this.state.imagesForPreview.map(url =>
                                <div>
                                    <img src={url} alt="failed load image" style={{width:"100%"}} />
                                </div>
                            )}
                        </Gallery>

                        <FormLayoutGroup>
                            <Checkbox name="isAnonymous">Опубликовать анонимно</Checkbox>
                            <Checkbox onChange={this.checkboxChanged.bind(this)}>
                                Согласен со всем <Link>этим</Link>
                            </Checkbox>
                        </FormLayoutGroup>

                        <Button type="submit" disabled={this.state.buttonEnabled} size="xl">
                            Опубликовать
                        </Button>

                        <input type="hidden" name="userId" value={fetchedUserId}/>
                    </FormLayout>

                </Panel>
            </View>
        )
    }
}

export default CreateView