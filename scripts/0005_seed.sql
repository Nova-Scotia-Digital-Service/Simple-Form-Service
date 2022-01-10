--"a7b65d0f-5b87-4050-a5ef-ef79ef0ec753" -- special patient program
--"170e324d-7aa9-4faf-8b42-5a7f6363fda5" -- early childhood development service
--"17fcd5c7-84cf-4cdc-bb2f-5649396ae8c6" -- drug information system
--"0d9fe6dc-a167-4fe1-9de4-dac996a44ac8" -- child care subsidy review
--"a84c16c0-5ea6-4cc0-87ae-2fdd575715e0" -- child care subsidy

DO $$	
BEGIN
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES ('a7b65d0f-5b87-4050-a5ef-ef79ef0ec753', '{ "Name": "Special Patient Program", "AuthorizedUsers": [ { "EmailAddress": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "EmailAddress": "Inica.Yang@devtestns.onmicrosoft.com" }, { "EmailAddress": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), 'a7b65d0f-5b87-4050-a5ef-ef79ef0ec753', '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "NotifyEmailAddresses": [ { "EmailAddress": "sclaus@northpole.com"	}, { "EmailAddress": "ebunny@rabbithole.com" } ] }');
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES ('170e324d-7aa9-4faf-8b42-5a7f6363fda5', '{ "Name": "Early Childhood Development Service", "AuthorizedUsers": [ { "EmailAddress": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "EmailAddress": "Inica.Yang@devtestns.onmicrosoft.com" }, { "EmailAddress": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), '170e324d-7aa9-4faf-8b42-5a7f6363fda5', '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "NotifyEmailAddresses": [ { "EmailAddress": "sclaus@northpole.com"	}, { "EmailAddress": "ebunny@rabbithole.com" } ] }');
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES ('17fcd5c7-84cf-4cdc-bb2f-5649396ae8c6', '{ "Name": "Drug Information System", "AuthorizedUsers": [ { "EmailAddress": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "EmailAddress": "Inica.Yang@devtestns.onmicrosoft.com" }, { "EmailAddress": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ] }');
    INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), '17fcd5c7-84cf-4cdc-bb2f-5649396ae8c6', '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "NotifyEmailAddresses": [ { "EmailAddress": "sclaus@northpole.com"	}, { "EmailAddress": "ebunny@rabbithole.com" } ] }');
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES ('0d9fe6dc-a167-4fe1-9de4-dac996a44ac8', '{ "Name": "Child Care Subsidy Review", "AuthorizedUsers": [ { "EmailAddress": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "EmailAddress": "Inica.Yang@devtestns.onmicrosoft.com" }, { "EmailAddress": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), '0d9fe6dc-a167-4fe1-9de4-dac996a44ac8', '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "NotifyEmailAddresses": [ { "EmailAddress": "sclaus@northpole.com"	}, { "EmailAddress": "ebunny@rabbithole.com" } ] }');
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES ('a84c16c0-5ea6-4cc0-87ae-2fdd575715e0', '{ "Name": "Child Care Subsidty", "AuthorizedUsers": [ { "EmailAddress": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "EmailAddress": "Inica.Yang@devtestns.onmicrosoft.com" }, { "EmailAddress": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), 'a84c16c0-5ea6-4cc0-87ae-2fdd575715e0', '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "NotifyEmailAddresses": [ { "EmailAddress": "sclaus@northpole.com"	}, { "EmailAddress": "ebunny@rabbithole.com" } ] }');
END $$

/*
DO $$
DECLARE
	specialPatientProgramTemplateId UUID = gen_random_uuid();
	earlyChildhoodDevelopmentServiceTemplateId UUID = gen_random_uuid();
	drugInformationSystemTemplateId UUID = gen_random_uuid();
	childCareSubsidyReviewTemplateId UUID = gen_random_uuid();
	childCareSubsidyTemplateId UUID = gen_random_uuid();	
BEGIN
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES (specialPatientProgramTemplateId, '{ "Name": "Special Patient Program", "AuthorizedUsers": [ { "EmailAddress": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "EmailAddress": "Inica.Yang@devtestns.onmicrosoft.com" }, { "EmailAddress": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), specialPatientProgramTemplateId, '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "NotifyEmailAddresses": [ { "EmailAddress": "sclaus@northpole.com"	}, { "EmailAddress": "ebunny@rabbithole.com" } ] }');
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES (earlyChildhoodDevelopmentServiceTemplateId, '{ "Name": "Early Childhood Development Service", "AuthorizedUsers": [ { "EmailAddress": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "EmailAddress": "Inica.Yang@devtestns.onmicrosoft.com" }, { "EmailAddress": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), earlyChildhoodDevelopmentServiceTemplateId, '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "NotifyEmailAddresses": [ { "EmailAddress": "sclaus@northpole.com"	}, { "EmailAddress": "ebunny@rabbithole.com" } ] }');
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES (drugInformationSystemTemplateId, '{ "Name": "Drug Information System", "AuthorizedUsers": [ { "EmailAddress": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "EmailAddress": "Inica.Yang@devtestns.onmicrosoft.com" }, { "EmailAddress": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), drugInformationSystemTemplateId, '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "NotifyEmailAddresses": [ { "EmailAddress": "sclaus@northpole.com"	}, { "EmailAddress": "ebunny@rabbithole.com" } ] }');
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES (childCareSubsidyReviewTemplateId, '{ "Name": "Child Care Subsidy Review", "AuthorizedUsers": [ { "EmailAddress": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "EmailAddress": "Inica.Yang@devtestns.onmicrosoft.com" }, { "EmailAddress": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), childCareSubsidyReviewTemplateId, '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "NotifyEmailAddresses": [ { "EmailAddress": "sclaus@northpole.com"	}, { "EmailAddress": "ebunny@rabbithole.com" } ] }');
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES (childCareSubsidyTemplateId, '{ "Name": "Child Care Subsidty", "AuthorizedUsers": [ { "EmailAddress": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "EmailAddress": "Inica.Yang@devtestns.onmicrosoft.com" }, { "EmailAddress": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), childCareSubsidyTemplateId, '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "NotifyEmailAddresses": [ { "EmailAddress": "sclaus@northpole.com"	}, { "EmailAddress": "ebunny@rabbithole.com" } ] }');
END $$
*/

/*
SELECT "Id", "Data"	FROM public."Form_Template";	
SELECT "Id", "Template_Id", "Data" FROM public."Form_Submission";
*/	

/*
DELETE FROM public."Form_Submission";
DELETE FROM public."Form_Template";
*/
	


	